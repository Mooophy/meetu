(function () {
    "use strict";
    angular
        .module('meetupModule')
        .controller('ProfileDisplayController', ProfileDisplayController);

    ProfileDisplayController.$inject = ["$log", "$q", "$resource", "$routeParams",'dummyDataService'];
    function ProfileDisplayController($log, $q, $resource, $routeParams, dummyDataService) {
        var vm = this,
            PublicProfile = $resource('api/Users/Public'),
            launchedMeetups = $resource('api/Meetups/LaunchedBy');

        vm.infoHasLoaded = false;
        vm.contentHasLoaded = false;

        launchedMeetups.query({ userId: 'b1d9d320-15cc-4d44-ad4d-9bd57d48ecd5' }, function (data) {
            vm.totalLaunchedMeetups = data;
        }).$promise.then(function () {
            vm.contentHasLoaded = true;
            vm.launchedMeetups = [];
            var launchedMeetups =[],
                i, len, currentMeetup, time;
            for (i = 0, len = vm.totalLaunchedMeetups.length; i < len; ++i) {
                if (!vm.totalLaunchedMeetups[i].meetup.isCancelled) {
                    vm.launchedMeetups.push(vm.totalLaunchedMeetups[i]);
                }
            }
            console.log(vm.launchedMeetups);
        });
           

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