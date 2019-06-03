var crypto = require('crypto');

module.exports = {
    encrypt_pass: function (password) {
        var hash = crypto.createHmac('sha1', 'abc').update(password).digest('hex'); 
        return hash;
    } 
}