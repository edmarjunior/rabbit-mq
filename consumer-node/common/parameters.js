module.exports = async () => {
    return {
        RabbitMq: {
            user: 'guest',
            password: 'guest',
            host: 'localhost',
            port: 5672
        }
    };
};
