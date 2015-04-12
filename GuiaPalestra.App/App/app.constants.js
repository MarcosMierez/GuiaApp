(function() {
    'use strict';
    var urls = {
        dashboard: 'inicio',
        api: 'http://localhost:6131/Api/'
    };

    var config = {
        url: urls
    };

   angular
        .module('MyApp')
        .constant('config', config);


})()