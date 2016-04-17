(function () {
    "use strict";
    angular
        .module('meetupModule')
        .controller('MeetupEditController', MeetupEditController)
    MeetupEditController.$inject = ["$rootScope", "$log", "$resource", "$location", "$scope", "$timeout"];
    function MeetupEditController($rootScope, $log, $resource, $location, $scope, $timeout) {
        var vm = this;
        $rootScope.$on("UpdateMeetup", function (event, message) {
            $log.debug(message);
            vm.editParams = message;
        });

        $timeout(function () {
            if (vm.editParams == null) {
                alert("edditing error");
                $location.path('/index');
            }
        });

        var Meetup = $resource('/api/Meetups', null, {
            'update': { method: 'PUT' }
        });
        vm.submitForm = function () {
            Meetup
                 // Please use GET first to fetch the newest object
                .get({ id: vm.editParams.id })
                .$promise
                .then(function (meetup) {
                    // Only the following 4 properties are allowed to update.
                    // Anything else will be ignored by back end
                    meetup.title = vm.editParams.title;
                    meetup.description = vm.editParams.description;
                    meetup.when = vm.editParams.when;
                    meetup.where = vm.editParams.where;

                    Meetup.update(meetup, function () {
                        $location.path('/index');
                    }, function () {
                        alert("update failed");
                        $location.path('/index');
                    })
                });
        }
        $(".js-meetup-edit-where").placepicker();
        $(".js-meetup-edit-when").datetimepicker({ minDate: '0' });
    }
})();