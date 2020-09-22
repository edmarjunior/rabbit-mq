module.exports = async (name, action) => {
    try {
        const parameters = await require('./common/parameters')(),
            RabbitMQ = require('./common/rabbit-mq');
        
        const queue = new RabbitMQ(parameters);

        console.log(`[✓] Worker: ${name} iniciado`);

        await action({ parameters, queue });
    } catch (e) {
       console.log(e);
    }
};
