var runway = {
  ready: function() {
    runway.placeholders();
    runway.newsList();
    runway.gallery();
    runway.cleanup();
  },
  
  // Convert labels to placeholder text.
  placeholders: function() {
    var $labels = $('label');
    if ($labels.length) {
      $labels.each(function() {
        var $this = $(this);
        var text = $.trim($this.addClass('invisible').text());
        if (text) {
          // Remove colon.
          text = text.replace(/:/g, '');
          if ($this.parent().is('div.form-label')) {
            $this.parent().next('div.form-input').find('input, textarea').attr('placeholder', text);
          }
          else {
            $this.next('input, textarea').attr('placeholder', text);
          }
        }
      });
    }
    $('input[placeholder], textarea[placeholder]').placeholder();
  },
  
  // Wrap all news items in a container.
  newsList: function() {
    var $list = $('div.newsList');
    if ($list.length) {
      // Remove unneeded breaks.
      $list.find('> br').remove();
      $list.find('h3').each(function(i) {
        $(this)
          .add($list.find('small')[i])
          .add($list.find('p')[i])
          .wrapAll('<div class="newsItem" />');
      });
    }
  },
  
  gallery: function() {
    var $images = $('a.lightbox');
    if ($images.length) {
      $images.colorbox();
    }
  },
  
  cleanup: function() {
    // Remove subnav without items.
    var $subnav = $('#subNavigation');
    if (!$subnav.find('li').length) {
      $subnav.remove();
    }
  }
};

$(document).ready(runway.ready);