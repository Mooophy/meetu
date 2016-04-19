(function() {
    "use strict";
    angular
        .module('meetupModule')
        .controller('MeetupCreateController', MeetupCreateController)

    MeetupCreateController.$inject = ["$log", "$q", "$resource", "$location"];
    function MeetupCreateController($log, $q, $resource, $location) {
        var vm = this;
        vm.createParams = {};
        vm.submitForm = function() {
            $resource('/api/loggedUser').query(function (userViews) {
                vm.createParams.sponsor = userViews[0].userId;
            }).$promise.then(function () {
                $log.debug(vm.createParams);
                $resource('/api/Meetups').save(vm.createParams)
                    .$promise.then(function () {
                        $log.debug(vm.createParams);
                        $location.path('/index');
                    });
            });
        }
    }
})();
