from confluent_kafka.admin import AdminClient

admin = AdminClient({
    "bootstrap.servers": "kafka:9092"
})

topic = "clean-developer-lerning-data"

try:
    metadata = admin.list_topics(timeout=5)

    if topic not in metadata.topics:
        exit(1)

    exit(0)

except Exception:
    exit(1)