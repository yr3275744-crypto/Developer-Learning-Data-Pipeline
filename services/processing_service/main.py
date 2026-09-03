import configuration
from procces_line import procces
import json


def acked(err, msg):
    if err is not None:
        print("Failed to deliver message: %s: %s" % (str(msg), str(err)))
    else:
        print("Message produced: %s" % (str(msg)))

def main():
    try:
        configuration.consumer.subscribe(configuration.consumer_topics)
        while True:
            msg = configuration.consumer.poll(timeout=1.5)
            if msg is None: continue

            if msg.error():
                print("kafka error")
           
            else:
                print(msg.value())
                line = json.loads(msg.value())
                # print(type(line))
                dict_result = procces(line)[0]
                proccesed_line = json.dumps(dict_result)
                print(proccesed_line)

                configuration.producer.produce(configuration.producer_topic, value=proccesed_line, callback=acked)
    finally:
        configuration.consumer.unsubscribe()
        configuration.consumer.close()
        configuration.producer.flush()


if __name__ == "__main__":
    main()