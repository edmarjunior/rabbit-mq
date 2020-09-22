const amqp = require('amqplib/callback_api');

module.exports = class RabbitMQ {
    constructor(parameters) {
        this.config = parameters.RabbitMq
    }

    connect = (queueName, callback, durable = true) => {
        const { user, password, host, port } = this.config;

        const connectionString = `amqp://${user}:${password}@${host}:${port}`;

        amqp.connect(connectionString, function (err, conn) {
            conn.createChannel(function (err, ch) {
                ch.assertQueue(queueName, { durable });
                ch.prefetch(1);
                
                console.log(`[...] Waiting for messages in queue: ` + queueName);
            
                ch.consume(queueName, async function (msg) {
                    console.log("[✓] Received message in queue: " + queueName);

                    await callback(msg.content.toString())
            
                    console.log('[✓] Finished message');
                    console.log(`[...] Waiting for messages in queue: ` + queueName);

                }, { noAck: true });
            });
        });
    }
}

