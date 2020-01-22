<?php
require "connect.php";
$json = json_decode($_POST['json']);
$username = $json->username;
$movementSpeed = $json->movementSpeed; // $json['username']
$attackSpeed = $json->attackSpeed;
$damage = $json->damage;
$health = $json->health;
$credits = $json->credits;
$response = new UpdateResponse();

$sql = "UPDATE accounts SET credits = '{$credits}', movementSpeed = '{$movementSpeed}', attackSpeed = '{$attackSpeed}', damage = '{$damage}', health = '{$health}' WHERE username = '{$username}'";
$result = $conn->query($sql);

$sql = "SELECT * FROM accounts WHERE username = '{$username}'";
$result = $conn->query($sql);

if($result->num_rows > 0)
{
      $row = $result->fetch_assoc();
      $response->credits = $row['credits'];
      $response->movementSpeed = $row['movementSpeed'];
      $response->attackSpeed = $row['attackSpeed'];
      $response->damage = $row['damage'];
      $response->health = $row['health'];
      $response->status = "check";
}
echo json_encode($response);

class Response
{
  public $status;
}
    class UpdateResponse extends Response
    {
      public $credits;
      public $movementSpeed;
      public $attackSpeed;
      public $damage;
      public $health;
    }
 ?>
