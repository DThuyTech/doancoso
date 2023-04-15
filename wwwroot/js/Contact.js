document.querySelector('#submit_form').onsubmit = function (e) {
    e.preventDefault();

    // khai bao de truy cap msg
    let msg0j = document.querySelector('.msg');
    //tat thong bao khi da nhap du lieu
    msg0j.innerText = '';
    //khai bao de truy cap den html
    let fullName0j = document.querySelector('input[name = "fullname"]');
    let email0j = document.querySelector('input[name = "email"]');
    let feedback0j = document.querySelector('input[name = "feedback"]');

    //lay gia tri cua user
    let fullName = fullName0j.value;
    let email = email0j.value;
    let feedback = feedback0j.value;

    //thong bao loi
    let errors = {}
    if (fullName.trim() == '') {
        errors['fullName'] = 'Họ và tên không được để trống !!!';
        fullName0j.parentElement.querySelector('.required').innerText = errors['fullName'];
    }
    if (email.trim() == '') {
        errors['email'] = 'Email không được để trống !!!';
        email0j.parentElement.querySelector('.required').innerText = errors['email'];
    }
    if (feedback.trim() == '') {
        errors['feedback'] = 'Mời bạn nhập ý kiến phản hồi !!!';
        feedback0j.parentElement.querySelector('.required').innerText = errors['feedback'];
    }

    // tat thong bao khi da nhap thong tin
    let required0j = document.querySelectorAll('.required');
    if (required0j.length > 0) {
        required0j.forEach(function (item) {
            item.innerText = '';
        })
    }

    // check loi ( = 0 thi khong co loi va nguoc lai)
    if (Object.keys(errors).length == 0) {
        let data = {
            'entry.1778650267': fullName,
            'entry.758964711': email,
            'entry.1855482181': feedback,
        }

        let queryString = new URLSearchParams(data);
        queryString = queryString.toString();
        let xhr = new XMLHttpRequest();
        xhr.open("POST", 'https://docs.google.com/forms/u/0/d/e/1FAIpQLSev-BKXZQl_qhmyxEKSgwBtDwowR3iUYPr1oayv-HD3M8LLrg/formResponse', true);
        xhr.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");

        msg0j.innerHTML = '<div class="alert alert-success text-center" > Gửi thành công </div>';
        //reset input khi submit
        fullName0j.value = '';
        email0j.value = '';
        feedback0j.value = '';

        xhr.send(queryString);
    }
    else {
        msg0j.innerHTML = '<div class="alert alert-danger text-center"> Vui lòng nhập đầy đủ thông tin </div>';
    }
}

