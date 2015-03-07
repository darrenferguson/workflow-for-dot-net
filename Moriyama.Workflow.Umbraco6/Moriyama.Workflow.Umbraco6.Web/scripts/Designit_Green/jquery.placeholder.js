/* 
* Placeholder plugin for jQuery
* @author Daniel Stocks (http://webcloud.se)
* @version 0.2
*/
(function($) {
    function Placeholder(input) {
        // Special treatment for password inputs
        if (input.attr('type') == 'password') {
            input.attr('realType', 'password');
            this.isPassword = true;
        }
        this.input = input;
        // IE doesn't allow changing the type of password inputs
        this.fakePassword = $('<input class="placeholder">').val(input.attr('placeholder')).focus(function() {
            input.trigger("focus");
            $(this).hide();
        });
    }
    Placeholder.prototype = {
        show : function(loading) {
            // FF and IE saves values when you refresh the page. If the user refreshes the page with 
            // the placeholders showing they will be the default values and the input fields won't be empty.
            if (this.input[0].value === '' || (loading && this.valueIsPlaceholder())) {
                if (this.isPassword) {
                    try { // IE doesn't allow us to change the input type
                        this.input[0].setAttribute('type', 'text');
                    } catch (e) {
                        this.input.before(this.fakePassword.show()).hide();
                    }
                }
                this.input[0].value = this.input.attr('placeholder');
                this.input.addClass('placeholder');
            }
        },
        hide : function() {
            if (this.valueIsPlaceholder() && this.input.hasClass('placeholder')) {
                if (this.isPassword) {
                    try {
                        this.input[0].setAttribute('type', 'password');
                    } catch (e) { }
                    // Restore focus for Opera and IE
                    this.input.show();
                    this.input[0].focus();
                }
                this.input[0].value = '';
                this.input.removeClass('placeholder');
            }
        },
        valueIsPlaceholder : function() {
            return this.input[0].value == this.input.attr('placeholder');
        }
    };
    var supported = !!("placeholder" in document.createElement( "input" ));
    $.fn.extend({
        placeholder: function() {
            return this.each(function() {
                if(!supported) {
                    var input = $(this);
                    var placeholder = new Placeholder(input);
                    placeholder.show(true);
                    input.focus(function() {
                        placeholder.hide();
                    });
                    input.blur(function() {
                        placeholder.show(false);
                    });
                }
            });
        }
    });
})(jQuery);