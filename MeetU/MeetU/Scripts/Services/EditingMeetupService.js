(function () {
    "use strict";
    angular
        .module('meetupModule')
        .factory('EditingMeetupService', EditingMeetupService)
    function EditingMeetupService() {
         var currentlyEditing = {};
         var flag = "isEditing";
         return {
             getEditingMeetup: function () {
                 if (flag = "hasEdited") {
                     return currentlyEditing;
                     flag = "editing";
                 } else {
                     console.log("getEditingMeetup Error");
                 }
             },
             setEditingMeetup: function (value) {
                 if (flag = "editing") {
                     currentlyEditing = value;
                     flag = "hasEdited";
                 } else {
                     console.log("setEditingMeetup Error");
                 }
             },
             clearEditingMeetup: function () {
                 currentlyEditing = {}
                 flag = "hasEditing";
             }
         }
     }   
})();