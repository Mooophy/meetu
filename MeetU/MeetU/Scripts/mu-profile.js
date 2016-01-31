var pm = angular.module('profileModule', ['ngResource']);
pm.controller('profileController', function ($scope, $http, $resource) {
    alert("from profile controller ");
});