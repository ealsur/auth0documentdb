'use strict';

(function () {
    angular.module('auth0App', ['ui.bootstrap', 'ui.toggle'])
        .config(['$httpProvider', function ($httpProvider) {
		    /*CONFIGURO EL PROVIDER PARA NUNCA CACHEAR RESPUESTAS DE GET*/
		    if (!$httpProvider.defaults.headers.get) {
		        $httpProvider.defaults.headers.get = {};
		    }
		    $httpProvider.defaults.headers.get['If-Modified-Since'] = 'Mon, 26 Jul 1997 05:00:00 GMT';
		    $httpProvider.defaults.headers.get['Cache-Control'] = 'no-cache';
		    $httpProvider.defaults.headers.get['Pragma'] = 'no-cache';
		    $httpProvider.defaults.headers.get['X-Requested-With'] = 'XMLHttpRequest';
        }])
		.controller('ProfileController', ['$http',
            function ($http) {
                var ctrl = this;
                /******** MODEL ************/
                ctrl.addresses = [];
                ctrl.notificationpreferences = {};
                ctrl.loadingNotifications=true;
                ctrl.loadingAddresses=true;
                /******** /MODEL ***********/
                ctrl.addAddress = function(){
                    ctrl.addresses.push({primary:false, address:''});
                }
                ctrl.notificationschanged = function(){
                    ctrl.loadingNotifications=true;
                    $http({
                        method: 'POST',
                        url: '/profile/notificationpreferences',
                        headers: { "Content-Type": "application/json" },
                        data:ctrl.notificationpreferences
                    }).then(function (response) {
                        ctrl.loadingNotifications=false;
                        ctrl.notificationpreferences =response.data;
                    });
                }
                ctrl.addressChanged = function(address){
                    ctrl.loadingAddresses=true;
                    $http({
                        method: 'POST',
                        url: '/profile/ContactAddresses',
                        headers: { "Content-Type": "application/json" },
                        data:address
                    }).then(function (response) {
                        ctrl.loadingAddresses=false;
                        address.id =response.data.id;
                        address.user =response.data.user;
                    });
                }
                ctrl.removeAddress = function(address, index){
                    ctrl.loadingAddresses=true;
                    if(address.hasOwnProperty("id")){
                         $http({
                            method: 'DELETE',
                            url: '/profile/RemoveContactAddresses/'+address.id
                        }).then(function (response) {
                            ctrl.loadingAddresses=false;
                             delete ctrl.addresses.splice(index,1);
                        });
                    }
                    else{
                    delete ctrl.addresses.splice(index,1);
                    }
                }
                $http({
                        method: 'GET',
                        url: '/profile/notificationpreferences',
                        headers: { "Content-Type": "application/json" }
                    }).then(function (response) {
                        ctrl.loadingNotifications=false;
                        ctrl.notificationpreferences =response.data;
                    });
                $http({
                        method: 'GET',
                        url: '/profile/contactaddresses',
                        headers: { "Content-Type": "application/json" }
                    }).then(function (response) {
                        ctrl.loadingAddresses=false;
                        ctrl.addresses =response.data.results;
                    });    
            }]);
})();