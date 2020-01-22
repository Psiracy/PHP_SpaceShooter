<?php
require "connect.php";

$json = json_decode($_POST['json']);
$username = $json->username; // $json['username']
$password = $json->password;
$token = GenerateToken(128);
$response = new LoginResponse();

$sql = "SELECT * FROM accounts WHERE username = '{$username}'";
$result = $conn->query($sql);

if($result->num_rows > 0)
{
  $row = $result->fetch_assoc();
      //password
    if(password_verify($password, $row["hash"]) === true)
    {
      $response->status = "logged in";
      //token
      $sql = "UPDATE accounts SET token = '{$token}' WHERE id = '{$row['id']}'";
      $response->credits = $row['credits'];
      $response->movementSpeed = $row['movementSpeed'];
      $response->attackSpeed = $row['attackSpeed'];
      $response->damage = $row['damage'];
      $response->health = $row['health'];
      $result = $conn->query($sql);
    }
    else
     {
    $response->status = "password incorrect";
    }

} else
 {
    $response->status = "username doesnt exist";
}

echo json_encode($response);

class Response
{
  public $status;
}

class LoginResponse extends Response
{
  public $credits;
  public $movementSpeed;
  public $attackSpeed;
  public $damage;
  public $health;
}



function GenerateToken($length)
{
  $characters = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
  $string = "";
  for ($i=0; $i < $length; $i++)
   {
    $string.=$characters[rand(0, strlen($characters)- 1)];
  }
  return $string;
}
 ?>
