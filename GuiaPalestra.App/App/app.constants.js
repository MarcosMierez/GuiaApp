(function() {
    'use strict';
    var urls = {
        dashboard: 'inicio',
        api: 'http://testeapi-2.apphb.com/Api/NoticiaApi'
    };

    var config = {
        url: urls
    };

   angular
        .module('MyApp')
        .constant('config', config);


})()