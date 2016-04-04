/*
This file in the main entry point for defining Gulp tasks and using Gulp plugins.
Click here to learn more. http://go.microsoft.com/fwlink/?LinkId=518007
*/

var gulp = require('gulp');
var minify = require('gulp-minify');


gulp.task('minifyjs', function () {
    gulp.src('Scripts/*.js')
      .pipe(minify({
          ignoreFiles: ['.combo.js', '-min.js']
      }))
      .pipe(gulp.dest('dist'))
});

gulp.task('minifyjs2', function () {
    gulp.src('Scripts/Controllers/**/*.js')
      .pipe(minify({
          ignoreFiles: ['.combo.js', '-min.js']
      }))
      .pipe(gulp.dest('dist/Controllers'))
});

gulp.task('minifyjs3', function () {
    gulp.src('Scripts/Directives/**/*.js')
      .pipe(minify({
          ignoreFiles: ['.combo.js', '-min.js']
      }))
      .pipe(gulp.dest('dist/Directives'))
});

gulp.task('minifyjs4', function () {
    gulp.src('Scripts/Helper/**/*.js')
      .pipe(minify({
          ignoreFiles: ['.combo.js', '-min.js']
      }))
      .pipe(gulp.dest('dist/Helper'))
});

gulp.task('minifyjs5', function () {
    gulp.src('Scripts/Router/**/*.js')
      .pipe(minify({
          ignoreFiles: ['.combo.js', '-min.js','min.js']
      }))
      .pipe(gulp.dest('dist/Router'))
});

gulp.task('minifystyles', function () {
    console.log('test');
});

gulp.task('watch', function () {
    gulp.watch('Scripts/*.js', ['minifyjs', 'minifyjs2', 'minifyjs3', 'minifyjs4', 'minifyjs5']);
})

gulp.task('default', ['minifyjs', 'minifyjs2', 'minifyjs3', 'minifyjs4', 'minifyjs5', 'watch']);