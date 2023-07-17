document.querySelector('#submit_form').onsubmit = function (e) {
    e.preventDefault();

    // khai bao de truy cap msg
    let msg0j = document.querySelector('.msg');
    //tat thong bao khi da nhap du lieu
    msg0j.innerText = '';
    //khai bao de truy cap den html
    let fullName0j = document.querySelector('input[name = "fullname"]');
    let email0j = document.querySelector('input[name = "email"]');
    let phone0j = document.querySelector('input[name = "phone"]');
    let feedback0j = document.querySelector('input[name = "feedback"]');

    //lay gia tri cua user
    let fullName = fullName0j.value;
    let email = email0j.value;
    let phone = phone0j.value;
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
    if (email.trim() == '') {
        errors['phone'] = 'PhoneNumber không được để trống !!!';
        phone0j.parentElement.querySelector('.required').innerText = errors['phone'];
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
            'entry.1972636253': fullName,
            'entry.666038548': email,
            'entry.610757160': phone,
            'entry.1946116580': feedback,
        }
        console.log(data);

        let queryString = new URLSearchParams(data);
        queryString = queryString.toString();
        let xhr = new XMLHttpRequest();
        xhr.open("POST", 'https://docs.google.com/forms/u/0/d/e/1FAIpQLSc5wuLlsNIYdsBn-A6b8WKMsGSOuIMwe9FQePnCVoT_MCWwhQ/formResponse', true);
        xhr.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");

        msg0j.innerHTML = '<div class="alert alert-success text-center" > Gửi thành công </div>';
        //reset input khi submit
        fullName0j.value = '';
        email0j.value = '';
        phone0j.value = '';
        feedback0j.value = '';

        xhr.send(queryString);
    }
    else {
        msg0j.innerHTML = '<div class="alert alert-danger text-center"> Vui lòng nhập đầy đủ thông tin </div>';
    }
}

