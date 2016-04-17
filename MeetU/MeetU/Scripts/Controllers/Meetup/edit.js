(function () {
    "use strict";
    angular
        .module('meetupModule')
        .controller('MeetupEditController', MeetupEditController)
    MeetupEditController.$inject = ["$log", "$resource", "$location", "$routeParams"];
    function MeetupEditController($log, $resource, $location, $routeParams) {
        var vm = this;
        var Meetup = $resource('/api/Meetups', null, {
            'update': { method: 'PUT' }
        });
        Meetup.get({ id: $routeParams.id })
            .$promise
            .then(function (meetup) {
                vm.editParams = meetup
            });
        vm.submitForm = function () {
            Meetup.update({
                // id must be provided
                id: $routeParams.id,
                title: vm.editParams.title,
                description: vm.editParams.description,
                when: vm.editParams.when,
                where: vm.editParams.where
            },
            function () {
                $location.path('/index');
            },
            function () {
                alert("edited error");
                $location.path('/index');
            }
            );
        }

        $(".js-meetup-edit-where").placepicker();
        $(".js-meetup-edit-when").datetimepicker({ minDate: '0' });
    }
})();