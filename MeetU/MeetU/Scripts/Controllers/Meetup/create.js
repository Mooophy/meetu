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
                //quick and dirty fix for the where bug
                vm.createParams.where = document.querySelector('.js-meetup-create-where').value;
                $log.debug(vm.createParams);
                $resource('/api/Meetups').save(vm.createParams)
                    .$promise.then(function () {
                        $log.debug(vm.createParams);
                        $location.path('/index');
                    });
            });
        }

        // TODO: should be refactored to angular style 
        $(".js-meetup-create-where").placepicker();
        $(".js-meetup-create-when").datetimepicker({minDate: '0'});
    }
})();
