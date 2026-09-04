from confluent_kafka import Producer
import socket
import csv
from pathlib import Path
import json

conf = {'bootstrap.servers': 'kafka:9092',
        'client.id': socket.gethostname()}

producer = Producer(conf)

topic = "raw-developer-lerning-data"

file_path = Path(__file__).parent / "data" / "developer_ai_learning_raw.csv"

def acked(err, msg):
    if err is not None:
        print("Failed to deliver message: %s: %s" % (str(msg), str(err)))
    else:
        print("Message produced: %s" % (str(msg)))

def read_lerning_file(path):
    with open (path, "r", newline='') as csvfile:
        reader = csv.DictReader(csvfile)
        i = 1
        for line in reader:
            line = json.dumps(line)
            producer.produce(topic, value= line, callback=acked)
            print(i)
            i += 1
            
try:
    read_lerning_file(file_path)
except FileNotFoundError as e:
    print("file is not found:", e)
finally:
    producer.flush()