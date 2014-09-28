#! /bin/sh

# Ok, simple script to do this.

aclocal
automake --add-missing --gnu
autoconf

./configure $@
