package com.productScanner.services;

import com.fasterxml.jackson.databind.ObjectMapper;
import com.productScanner.model.ImagePreprocessingEvent;
import com.rabbitmq.client.*;

import java.io.IOException;

public class EventBusService {

    private final String EXCHANGE_TYPE = "direct";

    private final String QUEUE_NAME = "preprocesing-api-java";
    private final String RECEIVE_ROUTING_KEY= "ImagePreprocessingEvent";
    private final String SEND_ROUTING_KEY= "ImagePreprocessingResultEvent";
    private final String EXCHANGE_NAME = "product-scanner-event-bus";

    private  Channel _channel;

    private WebSemanticService _service;

    public  EventBusService() throws java.util.concurrent.TimeoutException, java.io.IOException, IllegalAccessException {
        setupExchange(getConnectionFactory());
        _service = new WebSemanticService();
    }

    public void listen() throws java.io.IOException{
        Consumer consumer = new DefaultConsumer(_channel){

            @Override
            public void handleDelivery(String consumerTag, Envelope envelope, AMQP.BasicProperties properties, byte[] body) throws IOException {
                String message = new String(body, "UTF-8");
                System.out.println(" [x] Received '" + message + "'");
                ImagePreprocessingEvent data = new ObjectMapper().readValue(message, ImagePreprocessingEvent.class);
                _service.findInvalidPositions(data);
            }
        };
        _channel.basicConsume(QUEUE_NAME, true, consumer);
        System.out.println(" [*] Waiting for logs. To exit press CTRL+C");
    }

    private void setupExchange(ConnectionFactory factory) throws java.util.concurrent.TimeoutException, java.io.IOException{
        Connection connection = factory.newConnection();
        _channel= connection.createChannel();
        _channel.exchangeDeclare(EXCHANGE_NAME, EXCHANGE_TYPE);
        _channel.queueDeclare(QUEUE_NAME, false, false, false, null);
        _channel.queueBind(QUEUE_NAME, EXCHANGE_NAME, RECEIVE_ROUTING_KEY);
    }

    private ConnectionFactory getConnectionFactory(){
        ConnectionFactory factory = new ConnectionFactory();
        factory.setHost("localhost");
        factory.setUsername("node");
        factory.setPassword("node");
        return factory;
    }


}
