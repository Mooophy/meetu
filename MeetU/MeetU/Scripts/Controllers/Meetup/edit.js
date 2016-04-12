(function () {
    "use strict";
    angular
        .module('meetupModule')
        .controller('MeetupEditController', MeetupEditController)
    MeetupEditController.$inject = ["$log", "$q", "$resource", "$location", "$scope","MeetupDataService"];
    function MeetupEditController($log, $q, $resource, $location, $scope, MeetupDataService) {
        var vm = this;
        vm.editParams={}
        vm.editParams = MeetupDataService.getTemp();
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
                    console.log(r);
                }, function (r) {
                    console.log(r);
                })
                $location.path('/index');
            });
            }
        $(".js-meetup-edit-where").placepicker();
        $(".js-meetup-edit-when").datetimepicker({ minDate: '0' });
    }
})();