using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonogramAnswer : MonoBehaviour
{
    public bool[] firstStageAnswer = {false, false, true, true,  false,
                                      false, false, true, false, true,
                                      false, false, true, false, true,
                                      true,  true,  true, false, false,
                                      true,  true,  true, false, false};

    public bool[] secondStageAnswer = {false, false, true,  true,  true,  false, false,
                                       false, true,  false, false, false, true,  false,
                                       true,  false, true,  true,  true,  false, true,
                                       true,  false, false, false, false, false, true,
                                       false, true,  false, false, false, true,  false,
                                       false, false, true,  true,  true,  false, false,
                                       false, false, true,  false, true,  false, false,
                                       false, false, true,  false, true,  false, false,
                                       false, false, true,  false, true,  false, false,
                                       false, false, true,  false, true,  false, false,
                                       false, false, true,  false, true,  false, false,
                                       false, false, true,  false, true,  false, false,
                                       false, false, true,  true,  true,  false, false,
                                       false, false, false, true,  false, false, false,
                                       false, false, true,  false, false, false, false,
                                       false, false, true,  false, false, false, false,
                                       false, false, false, true,  true,  false, false};

    public bool[] thirdStageAnswer = {false, false, false, false, false, false, false, false, false, false, false, false, true,  false,
                                      false, false, false, false, false, false, false, false, false, false, false, true,  false, true,
                                      false, false, false, false, false, false, false, false, false, false, true,  false, false, true,
                                      false, false, false, false, false, false, false, false, false, false, true,  false, false, true,
                                      false, false, false, false, false, false, true,  false, false, true,  false, true,  true,  false,
                                      false, false, false, false, false, true,  true,  false, true,  false, true,  false, false, false,
                                      false, false, false, false, false, true,  false, true,  false, true,  false, false, false, false,
                                      false, false, true,  true,  true,  false, true,  false, true,  false, false, false, false, false,
                                      false, true,  false, false, false, true,  false, true,  false, true,  true,  false, false, false,
                                      true,  false, false, false, true,  false, true,  false, true,  true,  false, false, false, false,
                                      true,  false, false, false, true,  true,  false, true,  false, false, false, false, false, false,
                                      true,  false, false, false, false, false, false, true,  false, false, false, false, false, false,
                                      false, true,  false, false, false, false, false, true,  false, false, false, false, false, false,
                                      false, false, true,  false, false, false, true,  false, false, false, false, false, false, false,
                                      false, false, false, true,  true,  true,  false, false, false, false, false, false, false, false};
}
