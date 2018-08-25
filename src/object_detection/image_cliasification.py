import pika
import json
from Object_detection_image import ObjectDetection


class Communication:
    objectDetection = None

    exchange_type = 'direct'
    credentials = pika.PlainCredentials('node', 'node')
    connection = pika.BlockingConnection(pika.ConnectionParameters('localhost', credentials=credentials))
    channel = connection.channel()
    queue_name = 'scanner-api-python'
    receive_routing_key = 'ImageClasificationIntegrationEvent'
    send_routing_key = 'ImageClasificationResultIntegrationEvent'
    exchange_name = 'product-scanner-event-bus'

    def __init__(self):
        self.objectDetection = ObjectDetection()
        self.channel.exchange_declare(
            exchange=self.exchange_name,
            exchange_type=self.exchange_type)
        self.connect()

    def connect(self):
        self.channel.queue_declare(queue=self.queue_name, exclusive=False)
        self.channel.queue_bind(exchange='product-scanner-event-bus',
                                queue=self.queue_name,
                                routing_key=self.receive_routing_key)

        print(' [*] Waiting for logs. To exit press CTRL+C')

        self.channel.basic_consume(self.callback,
                                   queue=self.queue_name,
                                   no_ack=True)

        self.channel.start_consuming()

    def callback(self, ch, method, properties, data):
        body = str(data, "utf-8")
        model = json.loads(body)
        classification_result = self.objectDetection.analyse(model['Path'], False)
        classification_result.set_id((model['Id']))
        result_body = classification_result.to_message()

        self.channel.basic_publish(
            exchange=self.exchange_name,
            routing_key=self.send_routing_key,
            body=result_body
        )




def main():
    Communication()


if __name__ == "__main__":
    main()
