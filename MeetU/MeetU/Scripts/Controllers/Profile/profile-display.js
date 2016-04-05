(function () {
    "use strict";
    angular
        .module('meetupModule')
        .controller('ProfileDisplayController', ProfileDisplayController);

    ProfileDisplayController.$inject = ["$log", "$q", "$resource", "$routeParams"];
    function ProfileDisplayController($log, $q, $resource, $routeParams) {
        var vm = this;
        vm.hasLoaded = false;
        var Users = $resource('/api/Users');
        vm.participateList = [
            {
                title: "淫乱大趴",
                description: "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Praesent ultrices enim ac augue sodales facilisis. Ut in sollicitudin metus. Nulla tincidunt sapien ac eros tristique, eu aliquet arcu posuere. Etiam eget urna malesuada, iaculis erat at, sollicitudin lorem. Donec at suscipit ante. ",
                host: "刘宽"
            },
            {
                title: "学术大趴",
                description: "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Praesent ultrices enim ac augue sodales facilisis. Ut in sollicitudin metus. Nulla tincidunt sapien ac eros tristique, eu aliquet arcu posuere. Etiam eget urna malesuada, iaculis erat at, sollicitudin lorem. Donec at suscipit ante. ",
                host: "大哥"
            },
            {
                title: "热闹就好大趴",
                description: "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Praesent ultrices enim ac augue sodales facilisis. Ut in sollicitudin metus. Nulla tincidunt sapien ac eros tristique, eu aliquet arcu posuere. Etiam eget urna malesuada, iaculis erat at, sollicitudin lorem. Donec at suscipit ante. ",
                host: "克里斯"
            },
        ];

        vm.hostList = [
            {
                title: "岛国动作片赏观会",
                description: "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Praesent ultrices enim ac augue sodales facilisis. Ut in sollicitudin metus. Nulla tincidunt sapien ac eros tristique, eu aliquet arcu posuere. Etiam eget urna malesuada, iaculis erat at, sollicitudin lorem. Donec at suscipit ante. "
            },
            {
                title: "米国动作片赏观会",
                description: "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Praesent ultrices enim ac augue sodales facilisis. Ut in sollicitudin metus. Nulla tincidunt sapien ac eros tristique, eu aliquet arcu posuere. Etiam eget urna malesuada, iaculis erat at, sollicitudin lorem. Donec at suscipit ante. "
            },
            {
                title: "猎奇动作片赏观会",
                description: "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Praesent ultrices enim ac augue sodales facilisis. Ut in sollicitudin metus. Nulla tincidunt sapien ac eros tristique, eu aliquet arcu posuere. Etiam eget urna malesuada, iaculis erat at, sollicitudin lorem. Donec at suscipit ante. "
            }
        ]
        //  GET
        Users.get({ userId: $routeParams.profileId }, function(user) {
            vm.userData = user;
        }).$promise.then(function() {
            $log.debug("User: get user data:");
            $log.debug(vm.userData);
            $log.debug(vm.dummyList);
            vm.hasLoaded = true;
        });

        
    }
})();