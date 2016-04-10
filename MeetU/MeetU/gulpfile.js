/*
This file in the main entry point for defining Gulp tasks and using Gulp plugins.
Click here to learn more. http://go.microsoft.com/fwlink/?LinkId=518007
*/

var gulp = require('gulp');
var minify = require('gulp-uglify');
var sourcemaps = require('gulp-sourcemaps');
var concat = require('gulp-concat');
var notify = require("gulp-notify");
var gutil = require("gulp-util");

var onError = function(error) {
    gutil.log(error.message);
};
gulp.task('minify-vender-scripts', function () {
    return gulp.src(['Scripts/Venders/underscore.js',
               'Scripts/Venders/jquery-1.10.2.js',
               'Scripts/Venders/angular.js',
               'Scripts/Venders/**/*.js',
    ])
        .pipe(sourcemaps.init())
        .pipe(concat('vender.js'))
        .pipe(minify())
        .pipe(sourcemaps.write())
        .pipe(gulp.dest('Scripts/MinifiedScripts'))
        .pipe(notify("Vender scripts minified"))
        .on('error', onError);
});

gulp.task('watch-vender-scripts', function () {
    return gulp.watch(['Scripts/Venders/**/*.js'], ['minify-vender-scripts']);
});

gulp.task('minify-meetu-scripts', function () {
    return gulp.src([ 'Scripts/Controllers/Meetup/index.js',
               'Scripts/Helper/template-cache-helper.js',
               'Scripts/Controllers/**/*.js',
               'Scripts/Router/**/*.js',
               'Scripts/VenderPlugins/angular-moment.js',
               'Scripts/VenderPlugins/ui-bootstrap-tpls-1.2.5.js',
               'Scripts/VenderPlugins/angular-confirm.js',
               'Scripts/VenderPlugins/**/*.js',
               'Scripts/Directives/**/*.js',
               'Scripts/Helper/**/*.js',
               'Scripts/Services/**/*.js'
    ])
        .pipe(sourcemaps.init())
        .pipe(concat('all.js'))
        .pipe(minify())
        .pipe(sourcemaps.write())
        .pipe(gulp.dest('Scripts/MinifiedScripts'))
        .pipe(notify("Meetu scripts minified"))
        .on('error', onError);;
});

gulp.task('watch-meetu-scripts', function () {
    return gulp.watch(['Scripts/**/*.js', '!Scripts/_references.js', '!Scripts/MinifiedScripts/*.js', '!Scripts/UnitTests/**/*.js','!Scripts/Venders/**/*.js'], ['minify-meetu-scripts']);
});

gulp.task('concat-unittest-scripts', function () {
    return gulp.src(['Scripts/UnitTests/tests/**/*.js'])
        .pipe(concat('script.js'))
        .pipe(gulp.dest('Scripts/UnitTests'))
        .pipe(notify("UnitTest scripts concatenated"))
        .on('error', onError);;
});

gulp.task('watch-unittest-scripts', function () {
    return gulp.watch(['Scripts/UnitTests/tests/**/*.js'], ['concat-unittest-scripts']);
});

gulp.task('default', ['minify-vender-scripts', 'watch-vender-scripts', 'minify-meetu-scripts', 'watch-meetu-scripts', 'concat-unittest-scripts', 'watch-unittest-scripts']);
