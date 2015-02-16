var createParam = function (params) {
    return params ? $.parseJSON(params) : '';
};

var ArrayUnique = function (tab) {
    var u = {}, a = [];
    for (var i = 0, l = tab.length; i < l; ++i) {
        if (u.hasOwnProperty(tab[i])) {
            continue;
        }
        a.push(tab[i]);
        u[tab[i]] = 1;
    }
    return a;
}

jQuery.validator.unobtrusive.adapters.add(
    'requiredviral',
    ['requiredemailviral', 'alreadyguests', 'meemailviral', 'sameemailviral'],
    function (options) {

        var requiredemailviral = createParam(options.params.requiredemailviral),
            alreadyguests = createParam(options.params.alreadyguests),
            meEmailViral = createParam(options.params.meemailviral),
            sameemailviral = createParam(options.params.sameemailviral);

        // Ajoute la règle pour gérer le cas des champs vides.
        if (requiredemailviral) {
            options.rules['requiredemailviral'] = requiredemailviral.param;
            options.messages['requiredemailviral'] = requiredemailviral.errorMessage;
        }
        
        // Ajoute la règle pour gérer le cas des invitations déjà invitées.
        if (alreadyguests) {
            options.rules['alreadyguests'] = alreadyguests.param;
            options.messages['alreadyguests'] = alreadyguests.errorMessage;
        }

        // Ajoute la règle pour gérer le cas d'envoi à son propre email.
        if (meEmailViral) {
            options.rules['meemailviral'] = meEmailViral.param;
            options.messages['meemailviral'] = meEmailViral.errorMessage;
        }

        // Ajoute la règle pour gérer le cas des emails identiques.
        if (sameemailviral) {
            options.rules['sameemailviral'] = sameemailviral.param;
            options.messages['sameemailviral'] = sameemailviral.errorMessage;
        }
    }
);


$.validator.addMethod('requiredemailviral', function (value, element, params) {
    if (!$.isArray(params))
        return true;

    var val = "";
    $.each(params, function (index, item) {
        val += $(item).val();
    });

    return (val + value) !== '';
});

var elementGuest = {
    messageBase: '',
    isInit: true
};
$.validator.addMethod('alreadyguests', function (value, element, params) {
    if (!params.length)
        return true;

    var exists = $.inArray(value, params) !== -1

    if (exists) {
        var settngs = $.data($('form')[0], 'validator').settings;
        if (elementGuest.isInit) {
            elementGuest.messageBase = settngs.messages[element.id].alreadyguests;
            elementGuest.isInit = false;
        }

        var msg = elementGuest.messageBase;
        settngs.messages[element.id].alreadyguests = msg.replace('{0}', value);
    }

    return !exists;
});

$.validator.addMethod('meemailviral', function (value, element, params) {
    if (!params || !value)
        return true;
    return value !== '' && params !== $.trim(value.toLowerCase());
});

$.validator.addMethod('sameemailviral', function (value, element, params) {
    if (!$.isArray(params) || !value)
        return true;

    var allEmails = [];
    if (value)
        allEmails.push(value);
    var jEmails = $(params.join(','));

    for (var i = 0, ct = jEmails.length; i < ct; i++) {
        if (jEmails[i].value)
            allEmails.push(jEmails[i].value);
    }
    
    return ArrayUnique(allEmails).length === allEmails.length;
});

var validateDate = function (day, month, year, classError) {

    var continueCheck = true;

    var error = requiredDate(day, month, year);
    continueCheck = error === '';

    if (continueCheck) {
        error = validDate(day, month, year);
        continueCheck = error === ''
    }

    if (continueCheck) {
        error = mineurDate(day, month, year);
        continueCheck = error === ''
    }

    $(classError).text(error);
    $(classError)
        .removeClass('field-validation-valid')
        .addClass('field-validation-error');

    return continueCheck;
}
var requiredDate = function (day, month, year) {
    return (!day || !month || !year) ? "Veuillez préciser votre date de naissance." : "";
};

var validDate = function (day, month, year) {
    return !Helper.isDate(day, month, year) ? "Votre date de naissance est incorrecte." : "";
};

var mineurDate = function (day, month, year) {
    return Helper.isMineur(new Date(year, month, day)) ? "Vous devais être majeur." : "";
}



