(function () {
    "use strict";
    angular
        .module('meetupModule')
        .controller('ProfileDisplayController', ProfileDisplayController);

    ProfileDisplayController.$inject = ["$log", "$q", "$resource", "$routeParams",'dummyDataService'];
    function ProfileDisplayController($log, $q, $resource, $routeParams, dummyDataService) {
        console.log(dummyDataService);
        var vm = this;
        vm.hasLoaded = false;
        //var Users = $resource('/api/Users');
        var PublicProfile = $resource('api/Users/Public');
        vm.participatedMeetups = dummyDataService.participatedMeetups();
        vm.hostedMeetups = dummyDataService.hostedMeetups();
        vm.loremIpsum = dummyDataService.loremIpsum;

        //  GET
        //Users.get({ userId: $routeParams.profileId }, function(user) {
        //    vm.userData = user;
        //}).$promise.then(function() {
        //    $log.debug("User: get user data:");
        //    $log.debug(vm.userData);
        //    $log.debug(vm.participatedMeetups);
        //    $log.debug(vm.hostedMeetups);
        //    vm.hasLoaded = true;
        //});

        PublicProfile.get(
            {
                userId: $routeParams.profileId,
                joinedAmount: 3,
                launchedAmount: 3
            }, function (publicProfile) {
                vm.publicProfile = publicProfile;
            }
        ).$promise.then(function () {
            $log.debug(vm.publicProfile);
            vm.hasLoaded = true;
        })

        
    }
})();