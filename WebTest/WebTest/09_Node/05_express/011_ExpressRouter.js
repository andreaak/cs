var express = require('express');
var app = express();

// Класс Router позволяет определить маршрут, в пределах которого можно создавать подмаршруты и задавать им обработчики
var router = express.Router();

router.route("/")
            .get(function(req, res){     
                res.send("List of products. Get method.");
            })
            .post(function(req, res){
                res.send("Product created. POST method.");
            });
router.route("/:id")
            .get(function(req, res){
     
                res.send(`Product ${req.params.id}`);
            });

app.use("/products", router);
 
app.get("/", function(req, res){     
    res.send("Главная страница");
});
 
app.listen(8080);