(function () {
	'use strict';
	var controllerId = 'CadastrarController';
	angular.module('MyApp').controller(controllerId, CadastrarController);

	CadastrarController.$inject = ['$scope','$state'];

	function CadastrarController($scope,$state) {
	    var vm = this;

	    vm.voltar = function () {
	        $state.go('inicio');
	    }
		
	}
})()