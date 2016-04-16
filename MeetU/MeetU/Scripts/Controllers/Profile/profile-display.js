(function () {
    "use strict";
    angular
        .module('meetupModule')
        .controller('ProfileDisplayController', ProfileDisplayController);

    ProfileDisplayController.$inject = ["$log", "$q", "$resource", "$routeParams",'dummyDataService'];
    function ProfileDisplayController($log, $q, $resource, $routeParams, dummyDataService) {
        var vm = this,
            PublicProfile = $resource('api/Users/Public'),
            MeetupsLaunchedBy = $resource('api/Meetups/LaunchedBy');
            MeetupsJoinedBy = $resource('api/Meetups/JoinedBy');

        vm.infoHasLoaded = false;
        vm.contentHasLoaded = false;

        $q.all([
            MeetupsLaunchedBy.query({ userId: $routeParams.profileId }, function (data) {
                vm.launchedMeetups = data;
            }).$promise,
            MeetupsJoinedBy.query({ userId: $routeParams.profileId }, function (data) {
                vm.joinedMeetups = data;
            }).$promise
        ]).then(function(){
            vm.contentHasLoaded = true;
        })
           

        //var gender = "<i class='fa fa-genderless'></i>";

        PublicProfile.get(
            {
                userId: $routeParams.profileId,
                joinedAmount: 3,
                launchedAmount: 3
            }, function (publicProfile) {
                vm.publicProfile = publicProfile;
            }
        ).$promise.then(function () {
            vm.infoHasLoaded = true;

        });
    }

    function convertGender(gender) {
        var genders = {
            "male": {
                "class": "fa-mars",
                "color": "rgb(54,169,224)"
            },
            "female": {
                "class": "fa-venus",
                "color": "rgb(232,30,116)"
            }
        }
        return genders[gender] || {
            "class": "fa-genderless",
            "color": "rgb(153,153,153)"
        }
    }
})();