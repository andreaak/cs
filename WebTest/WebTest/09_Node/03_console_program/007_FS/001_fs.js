var fs = require('fs');
 
console.log("Going to open file!");

// открыть файл
// open(filename, flag, callback)
    // filename - имя файла
    // flag - флаг, который указывает каким образом открывать файлы
        // r - открыть для чтения. Генерирует исключение при отсутствии файла;
        //r+ - открыть для чтения и записи. Генерирует исключение при отсутствии файла;
        //rs - открыть для чтения в синхронном режиме;
        //rs+ - открыть для чтения и записи в синхронном режиме;
        //w - открыть для записи. Если файл не существует, он будет создан. Если файл существует, его содержимое будет очищено;
        //w+ - открыть для чтения и записи. Если файл не существует, он будет создан. Если файл существует, его содержимое будет очищено;
        //а - открыть для записи в конец файла. Если файл не существует, он будет создан;
        //а+ - открыть для чтения и записи в конец файла. Если файл не существует, он будет создан
    // callback - функция, которая вызывается после завершения чтения 
fs.open('demofile.txt', 'w+', function (err, fd) {
    console.log('opening file!'); 
    if (err) {
        console.log(err);
    }
    else {
        
        // Метод write - альтернатива методу writeFile; позволяет записать данные в файл и принимает такие аргументы: 
            // fd - дескриптор файла 
            // buffer - данные в виде буфера или строки, 
            // offset, length - определяют часть буфера, которую следует записать в файл 
            // position - отступ от начала файла, куда будут записаны данные 
            // callback - функция, принимающая аргументы err, written, string 

        fs.write(fd, 'This is the file content!', { encoding: 'utf-8' }, function (err, written, string) {

            console.log('writing to file!'); 

            if (err) throw err;

            console.log(written); // written - количество байтов, которое потребовалось для записи данных 
            console.log(string);  // строка, записанная в файл 
        });

        var arr = new Uint16Array(1024); 
        var buf = Buffer.from(arr.buffer);

        // Метод read позволяет читать данные из файла, принимает аргументы: 
            // fd -дескриптор файла
            // buffer - буфер, в который будут помещены прочитанные данные 
            // offset, length - определяют часть буфера, которую следует записать в файл 
            // position - отступ от начала файла, данные которого считываются 
            // callback - функция, принимающая аргументы err, bytesRead, buffer
        fs.read(fd, buf, 0, buf.length, 0, function (err, bytes) {

            console.log('reading from file!'); 

            if (err) throw err; 

            console.log(bytes);
            console.log(buf.slice(0, bytes).toString()); 

        })

        // закрыть файл 
        fs.close(fd, function (err) {
            if (err) throw err;
            console.log('file closed!');
        })
    }
}); 