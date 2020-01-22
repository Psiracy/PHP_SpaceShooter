<?php
require "connect.php";

$json = json_decode($_POST['json']);
$username = $json->username; // $json['username']
$password = $json->password;
$hash = password_hash($password, PASSWORD_BCRYPT);

$response = new SignupResponse();

$sql = "SELECT * FROM accounts WHERE username = '{$username}'";
$result = $conn->query($sql);

if($result->num_rows == 0) { // dit wordt de check of het account al bestaat
  $sql = "INSERT INTO accounts (username, hash, credits, movementSpeed, attackSpeed, damage, health) VALUES ('{$username}','{$hash}', '0', '1', '1', '1', '1')";
  if ($result = $conn->query($sql) === true)
  {
    $response->status = "account has been made";
  }
}
 else
 {
  $response->status = "username already exists";
}
echo json_encode($response);

class Response
{
  public $status;
}

class SignupResponse extends Response
{
  public $credits;
  public $movementSpeed;
  public $attackSpeed;
  public $damage;
  public $health;
}
?>
