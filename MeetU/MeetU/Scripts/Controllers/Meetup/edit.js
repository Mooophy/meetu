(function () {
    "use strict";
    angular
        .module('meetupModule')
        .controller('MeetupEditController', MeetupEditController)
    MeetupEditController.$inject = ["$log", "$resource", "$location", "$scope", "$route", "EditingMeetupService"];
    function MeetupEditController($log, $resource, $location, $scope, $route, EditingMeetupService) {
        var vm = this;
        vm.editParams = {}
        vm.editParams = EditingMeetupService.getEditingMeetup();
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
                        EditingMeetupService.clearEditingMeetup();
                        $location.path('/index');
                        $route.reload();
                    }, function () {
                        alert("edditing error");
                        EditingMeetupService.clearEditingMeetup();
                        $location.path('/index');
                        $route.reload();
                    })
                });
        }
        $(".js-meetup-edit-where").placepicker();
        $(".js-meetup-edit-when").datetimepicker({ minDate: '0' });
    }
})();