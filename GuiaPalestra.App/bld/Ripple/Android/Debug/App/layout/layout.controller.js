(function() {
    'use strict';
    var controllerId = 'LayoutController';
    angular.module('MyApp').controller(controllerId, LayoutController);



    function LayoutController() {
        var vm = this;
        vm.Titulo = 'Minha String';
    }
})()