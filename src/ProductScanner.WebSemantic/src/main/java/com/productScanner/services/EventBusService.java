package com.productScanner.services;

import com.fasterxml.jackson.databind.ObjectMapper;
import com.productScanner.model.ImagePreprocessingEvent;
import com.productScanner.model.ImagePreprocessingEventResult;
import com.productScanner.model.ReportEventResult;
import com.rabbitmq.client.*;
import java.util.concurrent.*;

import java.io.IOException;

public class EventBusService {

    private final String EXCHANGE_TYPE = "direct";

    private final String QUEUE_NAME = "preprocesing-api-java";
    private final String RECEIVE_ROUTING_KEY= "ImagePreprocessingEvent";
    private final String REPORT_ROUTING_KEY = "ReportEvent";
    private final String REPORT_RESULT_ROUTING_KEY ="ReportResultEvent";
    private final String SEND_ROUTING_KEY= "ImagePreprocessingResultEvent";
    private final String EXCHANGE_NAME = "product-scanner-event-bus";

    private  Channel _channel;

    private WebSemanticService _service;

    public  EventBusService() throws TimeoutException, IOException {
        setupExchange(getConnectionFactory());
        _service = new WebSemanticService();
    }

    public void listen() throws java.io.IOException{
        Consumer consumer = new DefaultConsumer(_channel){

            @Override
            public void handleDelivery(String consumerTag, Envelope envelope, AMQP.BasicProperties properties, byte[] body) throws IOException {
                System.out.println("ConsumerTag: " + consumerTag);
                String message = new String(body, "UTF-8");
                String routingKey = envelope.getRoutingKey();
                System.out.println(" [x] Received '" + message + "' on routing key: " + routingKey);
                switch (routingKey){
                    case "ReportEvent":
                        _service.startResoing();
                        ReportEventResult reportResult = new ReportEventResult();

                        reportResult.TotalNumberOfIndividuals = _service.getTotalNumberOfIndividuals();
                        reportResult.TotalNumberOfInvalidIndividuals = _service.getTotalNumberOfInvalidIndividuals();
                        reportResult.DetectedProducts= _service.getNumberOfDetectedIndividuals();
                        byte[] reportBytes = new ObjectMapper().writeValueAsString(reportResult).getBytes("utf-8");

                        _channel.basicPublish(EXCHANGE_NAME, REPORT_RESULT_ROUTING_KEY, null, reportBytes );
                        break;
                    default:
                        ImagePreprocessingEvent data = new ObjectMapper().readValue(message, ImagePreprocessingEvent.class);
                        _service.addToOntology(data.Data, data.Id);
                        _service.startResoing();
                        ImagePreprocessingEventResult result = _service.getResult(data.Data, data.Id);
                        _service.save();
                        byte[] bytes = new ObjectMapper().writeValueAsString(result).getBytes("utf-8");
                        _channel.basicPublish( EXCHANGE_NAME, SEND_ROUTING_KEY, null, bytes);
                }
            }
        };
        _channel.basicConsume(QUEUE_NAME, true, consumer);
        System.out.println(" [*] Waiting for logs. To exit press CTRL+C");
    }

    private void setupExchange(ConnectionFactory factory) throws TimeoutException, IOException{
        Connection connection = factory.newConnection();
        _channel= connection.createChannel();
        _channel.exchangeDeclare(EXCHANGE_NAME, EXCHANGE_TYPE);
        _channel.queueDeclare(QUEUE_NAME, false, false, false, null);
        _channel.queueBind(QUEUE_NAME, EXCHANGE_NAME, RECEIVE_ROUTING_KEY);
        _channel.queueBind(QUEUE_NAME, EXCHANGE_NAME, REPORT_ROUTING_KEY);
    }

    private ConnectionFactory getConnectionFactory(){
        ConnectionFactory factory = new ConnectionFactory();
        factory.setHost("localhost");
        factory.setUsername("node");
        factory.setPassword("node");
        return factory;
    }
}
