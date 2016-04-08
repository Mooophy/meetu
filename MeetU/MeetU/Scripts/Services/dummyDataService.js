(function () {
    angular.module("meetupModule")
        .service('dummyDataService', function () {
            var loremIpsum = "Lorem ipsum dolor sit amet, consectetur "
            + "adipiscing elit. Praesent ultrices enim ac augue sodales "
            + "facilisis. Ut in sollicitudin metus. Nulla tincidunt "
            + "sapien ac eros tristique, eu aliquet arcu posuere. Etiam "
            + "eget urna malesuada, iaculis erat at, sollicitudin lorem. "
            + "Donec at suscipit ante. ";
            
            return {
                loremIpsum: loremIpsum,

                participatedMeetups: function () {
                    return [
                        {
                            title: "淫乱大趴",
                            description: loremIpsum,
                            host: "刘宽"
                        },
                        {
                            title: "学术大趴",
                            description: loremIpsum,
                            host: "大哥"
                        },
                        {
                            title: "没有主题热闹就好大趴",
                            description: loremIpsum,
                            host: "克里斯"
                        }
                    ];
                },

                hostedMeetups: function () {
                    return [
                        {
                            title: "岛国动作片赏观会",
                            description: loremIpsum,
                        },
                        {
                            title: "米国动作片赏观会",
                            description: loremIpsum,
                        },
                        {
                            title: "猎奇动作片赏观会",
                            description: loremIpsum,
                        }
                    ]
                }
            }
        });
})();