from confluent_kafka import Producer
import socket
import csv
from pathlib import Path
import json

conf = {'bootstrap.servers': 'localhost:9092',
        'client.id': socket.gethostname()}

producer = Producer(conf)

topic = "raw-developer-lerning-data"

file_path = Path(__file__).parents[2] / "data" / "developer_ai_learning_raw.csv"

def read_lerning_file(path):
    with open (path, "r", newline='') as csvfile:
        reader = csv.DictReader(csvfile)
        for line in reader:
            producer.produce(topic, value= json.dumps(line))
            # print(json.dumps(line))
try:
    read_lerning_file(file_path)
except FileNotFoundError as e:
    print("file is not found:", e)
finally:
    producer.flush()