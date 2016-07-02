(function() {
    "use strict";
    angular
        .module('meetupModule')
        .controller('MeetupCreateController', MeetupCreateController)

    MeetupCreateController.$inject = ["$log", "$q", "$resource", "$location", "$scope", "$api"];
    function MeetupCreateController($log, $q, $resource, $location, $scope, $api) {
        var vm = this;
        vm.createParams = {};
        vm.submitForm = function () {
            $scope.$broadcast('show-errors-event');
            if ($scope.meetupCreateForm.$invalid) {
                return;
            }
            $resource('/api/loggedUser').query(function (userViews) {
                vm.createParams.sponsor = userViews[0].userId;
            }).$promise.then(function () {
                $log.debug(vm.createParams);
                $api.meetup.get().save(vm.createParams)
                    .$promise.then(function () {
                        $log.debug(vm.createParams);
                        $location.path('/index');
                    });
            });
        }
    }
})();
