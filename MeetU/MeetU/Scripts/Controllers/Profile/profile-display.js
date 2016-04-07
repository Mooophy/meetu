(function () {
    "use strict";
    angular
        .module('meetupModule')
        .controller('ProfileDisplayController', ProfileDisplayController);

    ProfileDisplayController.$inject = ["$log", "$q", "$resource", "$routeParams"];
    function ProfileDisplayController($log, $q, $resource, $routeParams) {
        var vm = this;
        vm.hasLoaded = false;
        var Users = $resource('/api/Users');
        //  GET
        Users.get({ userId: $routeParams.profileId }, function(user) {
            vm.userData = user;
        }).$promise.then(function() {
            $log.debug("User: get user data:");
            $log.debug(vm.userData);
            vm.hasLoaded = true;
        });
    }
})();