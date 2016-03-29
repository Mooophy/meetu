(function () {
    "use strict";
    angular
        .module('meetupModule')
        .controller('ProfileDisplayController', ProfileDisplayController)

    ProfileDisplayController.$inject = ["$log", "$q", "$resource", "$location"];
    function ProfileDisplayController($log, $q, $resource, $location) {
        var vm = this;
    }
})();