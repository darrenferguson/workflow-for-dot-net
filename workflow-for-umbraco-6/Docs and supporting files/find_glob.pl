use strict;
use utf8;

use File::Find::Rule;
use File::Slurp;
use Data::Dumper;

my @files = File::Find::Rule->file()
                              ->name( '*.ascx' )
                              ->in( '.' );

my $keys;
foreach  (@files) {

	my $data = read_file($_);

	while($data =~ s|\.GetString\(\"(.*?)\"||) {
		
		$keys->{$1} = $1;
	}	

}

my @vals = keys %$keys;
@vals = sort { $a cmp $b } @vals;


foreach (@vals) {

	my $v = $_;
	$v =~ s|\_| |g;
	$v = ucfirst($v) .'.';
	
	$v =~ s/\b(\w)/\U$1/g;

	print qq(	<entry key="$_">
          <value>$v</value>
        </entry>
);

}