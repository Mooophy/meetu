(function () {
    "use strict";
    angular
        .module('meetupModule')
        .controller('mainNavbarController', mainNavbarController);

    mainNavbarController.$inject = ["$log", "$resource"];
    function mainNavbarController($log, $resource) {
        var vm = this;
        var userview = $resource('/api/loggedUser');

        vm.logOut = function () {
            var logOut = new ($resource('/Account/LogOff'));
            logOut.$save(null, function () {
                window.location.reload(true);
            }, function (error) {
                $log.debug("log out failed: " + error);
            });
        }

        userview.query(function(userViews) {
            vm.loggedUser = userViews[0];
        });
    }
})();
