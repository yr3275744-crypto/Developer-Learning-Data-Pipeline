from confluent_kafka import Consumer
from confluent_kafka import Producer
import socket
import random

consumer_conf = {'bootstrap.servers': 'kafka:9092',
        'group.id': str(random.randint(0, 1000)),
        'auto.offset.reset': 'smallest'}

consumer = Consumer(consumer_conf)
consumer_topics = ["raw-developer-lerning-data"]


producer_conf = {'bootstrap.servers': 'kafka:9092',
        'client.id': socket.gethostname()}

producer = Producer(producer_conf)

producer_topic = "clean-developer-lerning-data"