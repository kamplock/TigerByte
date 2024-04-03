<?php
// Check for form submission
if (isset($_POST['name'])) {

  // Retrieve form data
  $name = $_POST['name'];
  $email = $_POST['email'];
  $subject = $_POST['subject'];
  $message = $_POST['message'];

  // Set recipient email address
  $to = 'olivia.navarroalsina@doane.edu'; // Replace with your actual email address

  // Email headers
  $headers = 'From: ' . $name . ' <' . $email . '>' . "\r\n";

  // Email body content
  $body = "You have received a new message from your contact form:\n\n";
  $body .= "Name: $name\n";
  $body .= "Email: $email\n";
  $body .= "Subject: $subject\n";
  $body .= "Message:\n$message";

  // Send email using PHP's mail() function
  if (mail($to, $subject, $body, $headers)) {
    echo "<script>
      document.querySelector('.loading').style.display = 'none';
      document.querySelector('.error-message').style.display = 'none';
      document.querySelector('.sent-message').style.display = 'block';
      document.querySelector('.php-email-form').reset();
    </script>";
  } else {
    echo "<script>
      document.querySelector('.loading').style.display = 'none';
      document.querySelector('.error-message').innerHTML = 'An error occurred while sending the email.';
      document.querySelector('.error-message').style.display = 'block';
    </script>";
  }
}
?>
