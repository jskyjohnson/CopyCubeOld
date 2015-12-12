using UnityEngine;
using System.Collections;

public class DirectionSwitcher : MonoBehaviour {
	public string direction;
	public bool mobOnly;

	void OnTriggerEnter(Collider coll) {
		if(coll.name == "Player" && mobOnly != true) {
			if(direction != Player.direction) {
				coll.gameObject.transform.position = new Vector3(Mathf.Round (coll.gameObject.transform.position.x), Mathf.Round (coll.gameObject.transform.position.y), Mathf.Round(coll.gameObject.transform.position.z));
				coll.gameObject.transform.rotation = Quaternion.Euler(Vector3.zero);
			}
			if(direction == "+x") {
				Player.direction = "+x";
			} else if(direction == "+z") {
				Player.direction = "+z";
			} else if(direction == "-x") {
				Player.direction = "-x";
			} else if (direction == "-z") {
				Player.direction = "-z";
			}
			if(direction == "+x" || direction == "-x") {
				GameObject.Find ("Player").GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationX;
			} else if(direction == "+z" || direction == "-z") {
				GameObject.Find ("Player").GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
			}
		}
		if(coll.name == "FakePlayer") {
			if(direction == "+x") {
				coll.gameObject.GetComponent<FakeHomePlayer>().direction = "+x";
				coll.gameObject.transform.rotation = Quaternion.Euler(Vector3.zero);
			} else if(direction == "+z") {
				coll.gameObject.GetComponent<FakeHomePlayer>().direction = "+z";
				coll.gameObject.transform.rotation = Quaternion.Euler(Vector3.zero);
			}
		}
		if(coll.name == "Snowman") {
			if(direction == "+x") {
				coll.gameObject.GetComponent<Snowman>().direction = "+x";
				coll.transform.rotation = Quaternion.LookRotation(new Vector3(1f, 0f, 0f));
			} else if(direction == "+z") {
				coll.gameObject.GetComponent<Snowman>().direction = "+z";
				coll.transform.rotation = Quaternion.LookRotation(new Vector3(0f, 0f, 1f));
			} else if(direction == "-x") {
				coll.gameObject.GetComponent<Snowman>().direction = "-x";
				coll.transform.rotation = Quaternion.LookRotation(new Vector3(-1f, 0f, 0f));
			} else if (direction == "-z") {
				coll.gameObject.GetComponent<Snowman>().direction = "-z";
				coll.transform.rotation = Quaternion.LookRotation(new Vector3(0f, 0f, -1f));
			}
			/*
			 * if(direction == "+x") {
				if(coll.gameObject.GetComponent<Snowman>().direction == "+z") {
					coll.transform.Rotate(0,90,90);
				} else if (coll.gameObject.GetComponent<Snowman>().direction == "-z") {
					coll.transform.Rotate(0,-90f,0);
				}
				coll.gameObject.GetComponent<Snowman>().direction = "+x";
			} else if(direction == "+z") {
				if(coll.gameObject.GetComponent<Snowman>().direction == "+x") {
					coll.transform.Rotate(0,-90,0);
				} else if (coll.gameObject.GetComponent<Snowman>().direction == "-x") {
					coll.transform.Rotate(0,90f,0);
				}
				coll.gameObject.GetComponent<Snowman>().direction = "+z";
			} else if(direction == "-x") {
				if(coll.gameObject.GetComponent<Snowman>().direction == "+z") {
					coll.transform.Rotate(0,90,0);
				} else if (coll.gameObject.GetComponent<Snowman>().direction == "-z") {
					coll.transform.Rotate(0,-90f,0);
				}
				coll.gameObject.GetComponent<Snowman>().direction = "-x";
			} else if (direction == "-z") {
				if(coll.gameObject.GetComponent<Snowman>().direction == "+x") {
					coll.transform.Rotate(0,90,0);
				} else if (coll.gameObject.GetComponent<Snowman>().direction == "-x") {
					coll.transform.Rotate(0,-90f,0);
				}
				coll.gameObject.GetComponent<Snowman>().direction = "-z";
			}*/
		}
		if(coll.name == "Turkey") {
			if(direction == "+x") {
				coll.gameObject.GetComponent<Turkey>().direction = "+x";
				coll.transform.rotation = Quaternion.LookRotation(new Vector3(1f, 0f, 0f));
			} else if(direction == "+z") {
				coll.gameObject.GetComponent<Turkey>().direction = "+z";
				coll.transform.rotation = Quaternion.LookRotation(new Vector3(0f, 0f, 1f));
			} else if(direction == "-x") {
				coll.gameObject.GetComponent<Turkey>().direction = "-x";
				coll.transform.rotation = Quaternion.LookRotation(new Vector3(-1f, 0f, 0f));
			} else if (direction == "-z") {
				coll.gameObject.GetComponent<Turkey>().direction = "-z";
				coll.transform.rotation = Quaternion.LookRotation(new Vector3(0f, 0f, -1f));
			}
		}
		if(coll.name == "Pylon") {
			if(direction == "+x") {
				coll.gameObject.GetComponent<Pylon>().direction = "+x";
			} else if(direction == "+z") {
				coll.gameObject.GetComponent<Pylon>().direction = "+z";
			} else if(direction == "-x") {
				coll.gameObject.GetComponent<Pylon>().direction = "-x";
			} else if (direction == "-z") {
				coll.gameObject.GetComponent<Pylon>().direction = "-z";
			}
		}
	}
}
