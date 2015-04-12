(function() {
    'use strict';

    angular.module('MyApp', ['ionic'])

        .run(function () {


        })

        .config(function ($urlRouterProvider, $stateProvider) {

        $stateProvider
            .state('inicio', {
                url: '/',
                views: {
                    '': { templateUrl: 'App/Layout/layout.body.html' },
                    'partial@inicio':{templateUrl :'App/inicio/inicio.html'}
                }
            })
            .state('cadastrar', {
                url: '/cadastrar',
                views: {
                    '': { templateUrl: 'App/Layout/layout.body.html' },
                    'partial@cadastrar': { templateUrl: 'App/cadastrar/cadastrar.html' }
                }
            });

        $urlRouterProvider.otherwise('/');

    });


})()