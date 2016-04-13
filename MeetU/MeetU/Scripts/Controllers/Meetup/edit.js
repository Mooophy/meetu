(function () {
    "use strict";
    angular
        .module('meetupModule')
        .controller('MeetupEditController', MeetupEditController)
    MeetupEditController.$inject = ["$log", "$resource", "$location", "$scope","$route", "EditingMeetupService"];
    function MeetupEditController($log, $resource, $location, $scope, $route, EditingMeetupService) {
        var vm = this;
        vm.editParams={}
        vm.editParams = EditingMeetupService.getEditing();
        vm.submitForm = function() {
        var Meetup = $resource('/api/Meetups', null, {
            'update': { method: 'PUT' }
        });

        Meetup
             // Please use GET first to fetch the newest object
            .get({ id: vm.editParams.id })
            .$promise
            .then(function (m) {
                // Only the following 4 properties are allowed to update.
                // Anything else will be ignored by back end
                m.title = vm.editParams.title;
                m.description = vm.editParams.description;
                m.when = vm.editParams.when;
                m.where = vm.editParams.where;

                Meetup.update(m, function (r) {
                    $log.debug(r)
                    EditingMeetupService.deleteEditing()
                }, function (r) {
                    $log.debug(r)
                })
                $location.path('/index');
                $route.reload();
            });
            }
        $(".js-meetup-edit-where").placepicker();
        $(".js-meetup-edit-when").datetimepicker({ minDate: '0' });
    }
})();