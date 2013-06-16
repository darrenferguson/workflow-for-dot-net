use strict;
use utf8;
use File::Find::Rule;

my $search = '<package id\=\"log4net\" version\=\"2\.0\.0\" targetFramework\=\"net40\" \/>';
my $replace = '<package id="Common.Logging" version="2.1.2" targetFramework="net40" />';

my @files = File::Find::Rule->file()
                              ->name( 'packages.config' )
                              ->in( '.');


foreach(@files) {

	print "$_\n";

	my $name = $_;
	local undef $/;

	open FILE, "<$name";
	my $data = <FILE>;
	close FILE;

	$data =~ s|$search|$replace|igsm;

	# print $data;


	open FILE, ">$name";
	print FILE $data;
	close FILE;



}