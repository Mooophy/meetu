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

    }
})();