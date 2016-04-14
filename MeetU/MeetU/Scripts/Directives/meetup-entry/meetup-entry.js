(function () {
    "use strict";
    angular
        .module("meetupModule")
        .directive('meetupEntry', function () {
            var controller = function() {
                var vm = this;

                init();

                console.log(vm);

                function init() {
                    var data = angular.copy(vm.datasource),
                        meetup = data.meetup,
                        joins = data.joinViews,
                        i, len, currentPerson;

                    vm.meetup = {
                        title: meetup.title,
                        description: meetup.description,
                        when: convertTime(meetup.when),
                        createdAt: convertTime(meetup.createdAt),
                        id: meetup.id,
                        sponsorId: meetup.sponsor,
                        sponsorUserName: data.sponsorUserName,
                        sponsorNickName: data.sponsorNickName,
                        updatedAt: convertTime(meetup.updatedAt),
                        where: meetup.where,
                    };

                    vm.joins = {
                        joinedNumber: joins.length,
                        joinedPeople: []
                    };

                    for (i = 0, len = joins.length; i < len; ++i) {
                        currentPerson = {};
                        currentPerson.name = joins[i].theJoinedUserNickName || joins[i].join.userName;
                        currentPerson.id = joins[i].join.userId;
                        currentPerson.joinedAt = convertTime(joins[i].join.at).completeTimeString;
                        vm.joins.joinedPeople.push(currentPerson);
                    }



                }



                function convertTime(timeString) {
                    if (!timeString) return null;
                    var weekdays = ["Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat"],
                        months = ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct","Nov","Dec"],
                        when = new Date(timeString),
                        now = new Date();

                    var time = convertTimeMeridiem(when.getHours()).hour + ":" + paddingZero(when.getMinutes()),
                        meridiem = convertTimeMeridiem(when.getHours()).meridiem,
                        weekday = weekdays[when.getDay()],
                        date = when.getDate() + ", " + months[when.getMonth()],
                        completeTimeString = time + " " + meridiem + " " + date + " " + when.getYear();

                    return {
                        time: time,
                        meridiem: meridiem,
                        weekday: weekday,
                        date: date,
                        expired: now > when ? true : false,
                        completeTimeString: completeTimeString
                    }
                }

                function convertTimeMeridiem(hour) {
                    return {
                        hour: hour > 12 ? hour - 12 : hour,
                        meridiem: hour > 12 ? "PM" : "AM"
                    }
                }

                function paddingZero(min) {
                    var minute = min.toString();
                    while (minute.length < 2) {
                        minute = "0" + minute;
                    }
                    return minute;
                }
                
            };


            return {
                restrict: 'EA',
                scope: {
                    datasource: "="
                },
                controller: controller,
                controllerAs: 'vm',
                bindToController: true,
                templateUrl: '/Scripts/Directives/meetup-entry/meetup-entry.html'
            };
        });
})();