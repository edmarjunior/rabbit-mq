require('../worker')('DigitalizacaoDocumentos', async ({ queue }) => {
    const axios = require('axios');
    const path = require('path') 
    const fs = require('fs');

    queue.connect('documentos_queue', handle);

    async function handle (content)  {
        const documentos = JSON.parse(content);

        for (const documento of documentos) {
            const pdf = await geraPdf(documento.Id);
            await uploadPdf(pdf, documento.Id);
            console.log(`Documento ${documento.Id} digitalizado!`)
        }

        console.log('Documentos digitalizados com sucesso!');
    }

    async function geraPdf () {  
        const response = await axios({
            method: 'get',
            url: 'https://www.w3.org/WAI/ER/tests/xhtml/testfiles/resources/pdf/dummy.pdf',
            responseType: 'stream'
        });

        return response.data;
    }

    async function uploadPdf (pdf, fileName) {
        if (!fs.existsSync('documentos-gerados')) {
            fs.mkdirSync('documentos-gerados');
        }

        const pathDocumento = path.resolve(__dirname, 'documentos-gerados', fileName + '.pdf');

        pdf.pipe(fs.createWriteStream(pathDocumento));
    }
});
