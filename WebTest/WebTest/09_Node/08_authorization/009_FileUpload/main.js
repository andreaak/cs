var express = require('express'); 

var port = 8080; 

var path = require('path'); 
var fs = require('fs'); 

var mssql = require('mssql'); 
var connection = require('./db_handler'); 


// multer - middleware для обработки данных формы в кодировке multipart/form-data 
var multer = require('multer'); 
// dest - директория для сохранения файлов, загружаемых на сервер 
var upload = multer({dest: __dirname + '/uploads'}); 

// указать, что будет загружен один файл с именем recfile. 
// имя файла может быть любым, но оно должно совпадать со значением атрибута name элемента формы input с типом file
// например, <input type="file" name="recfile" />
var type = upload.single('recfile'); 

var app = express(); 

app.get('/', function(req,res, next) {
	 res.sendFile(__dirname + '/index.html'); 

})

// указать файл при обработке POST запроса (второй аргумент метода app.post)
 app.post('/upload', type, function(req, res) { 
 
	 // загруженный файл доступен через свойство req.file
	 console.log(req.file); 
	 
	 // файл временного хранения данных
	 var tmp_path = req.file.path;
	 console.log(req.file.destination)
	 // место, куда файл будет загружен 
	 var target_path = path.join(req.file.destination, req.file.originalname); 
	
	  // загрузка файла 
	  var src = fs.createReadStream(tmp_path); 
	  var dest = fs.createWriteStream(target_path);
	  
	  src.pipe(dest); 
	  
	  // обработка результатов загрузки 
	  src.on('end', function() { 
	  
		// удалить файл временного хранения данных
		fs.unlink(tmp_path); 
		save_path(target_path, res); 

	  });
	  src.on('error', function(err) { 
	  
	  	// удалить файл временного хранения данных
	    fs.unlink(tmp_path);  
		res.send('error'); 
	  });

 }) 
 
 app.use(express.static(path.join(__dirname))); 
 
 app.get('/get_uploads', function(req, res) {
	 
	 get_images(res); 
 })
 
 function save_path(target_path, res) {
	 
	 var ps = new mssql.PreparedStatement(connection); 
	 ps.input('path', mssql.Text); 
	 
	 var inserts = {
		 path: target_path
	 } 
	 console.log(target_path)
	 ps.prepare('INSERT INTO images (src) VALUES (@path)', function(err) {
		 
		 if (err) console.log(err); 
		 
		 ps.execute(inserts, function(err, data) {
			if (err) console.log(err); 
			ps.unprepare(); 
		    res.send('complete'); 
		 })
	 })

 } 
 
 function get_images(res) {
	 
	 var request = new mssql.Request(connection); 
	
	 request.query('SELECT * FROM images', function(err, rows) {
		 var images = rows.map((row) => {
			 return `<img src="${row.src}" style="width:400px" />`
		 }); 
		 var img_arr = JSON.stringify(images); 
		 console.log(img_arr); 
		 res.send(img_arr); 
	 })
 }
 
 app.listen(port, function() {
	    console.log('App listening on port ' + port);
 }); 