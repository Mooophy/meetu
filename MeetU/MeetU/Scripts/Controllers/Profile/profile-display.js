(function () {
    "use strict";
    angular
        .module('meetupModule')
        .controller('ProfileDisplayController', ProfileDisplayController);

    ProfileDisplayController.$inject = ["$log", "$q", "$resource", "$routeParams"];
    function ProfileDisplayController($log, $q, $resource, $routeParams) {
        var vm = this,
            PublicProfile = $resource('api/Users/Public'),
            PrivateProfile = $resource('api/Users/Private'),
            MeetupsLaunchedBy = $resource('api/Meetups/LaunchedBy'),
            MeetupsJoinedBy = $resource('api/Meetups/JoinedBy');

        var User = $resource('/api/loggedUser');
        User.query(function (user) {
            vm.loggedUser = user[0];
        }).$promise.then(function () {
            console.log(vm.loggedUser);
        });

        init();
        
        PrivateProfile.get(
            {
                userId: $routeParams.profileId,
                joinedAmount: 3,
                launchedAmount: 3
            }, function (privateProfile) {
                vm.privateProfile = privateProfile;
            }
        ).$promise.then(function (data) {
            vm.infoHasLoaded = true;
            vm.isPrivate = true;
            console.log("isPrivate");
            console.log(vm.privateProfile);
            vm.profileInformation = getProfileInformation(data, vm.isPrivate);
        }, function (reason) {
            vm.isPrivate = false;
            return $q.reject(reason);
        }).then(null, function () {
            PublicProfile.get(
                {
                    userId: $routeParams.profileId,
                    joinedAmount: 3,
                    launchedAmount: 3
                }, function (publicProfile) {
                    vm.publicProfile = publicProfile;
                }
            ).$promise.then(function (data) {
                console.log("isPublic");
                console.log(vm.publicProfile);
                vm.infoHasLoaded = true;
                vm.profileInformation = data;
                vm.isPrivate = false;
                vm.profileInformation = getProfileInformation(data, vm.isPrivate);
                showJoinedList();
            });
        });
        

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

        vm.showPersonalInfo = showPersonalInformation;

        vm.showLaunchedList = showLaunchedList;

        vm.showJoinedList = showJoinedList;

        function showPersonalInformation() {
            vm.isPersonalInformation = true;
            vm.isLaunchedMeetups = false;
            vm.isJoinedMeetups = false;
            vm.activeMenu = "Personal Information";
        }

        function showLaunchedList() {
            vm.isPersonalInformation = false;
            vm.isLaunchedMeetups = true;
            vm.isJoinedMeetups = false;
            vm.activeMenu = "Launched Meetups";
        }

        function showJoinedList() {
            vm.isPersonalInformation = false;
            vm.isLaunchedMeetups = false;
            vm.isJoinedMeetups = true;
            vm.activeMenu = "Joined Meetups";
        }

        function init() {
            vm.infoHasLoaded = false;
            vm.contentHasLoaded = false;
            vm.isPrivate = false;

            showPersonalInformation();

            vm.profileInformation = {};
        }

        function getProfileInformation(profile, isPrivate) {
            var genderIcon = (profile.gender === null || profile.gender === "male") ? "fa-mars" : "fa-venus",
                genderColor = (profile.gender === null || profile.gender === "male") ? "color-male" : "color-female",
                defaultPicture = "/Content/Images/dummyHead.PNG"
                
            var profileInfo = {
                nickName: profile.nickName,
                brief: profile.brief || "...",
                gender:profile.gender || "genderless",
                genderIcon: genderIcon,
                genderColor: genderColor,
                createdAt: profile.createdAt,
                updatedAt: profile.updatedAt,
                email: profile.emal,
                meetupsJoined: profile.joinedMeetupsTotal,
                meetupsLaunched: profile.launchedMeetupsTotal,
                userNumber: profile.number,
                userId: profile.userId,
                userName: profile.userName,
                picture: profile.picture || defaultPicture,
                privateInfo: {}
            };

            if (isPrivate) {
                profileInfo.privateInfo = {
                    familyName: profile.givenName || "",
                    givenName: profile.giveName || "",
                    loginCount: profile.loginCount,
                }
            }

            return profileInfo;
        }


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