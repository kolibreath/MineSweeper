from flask import jsonify, request, current_app, url_for
from . import api
from .. import db
from ..models import User
from ..decorator import login_required

@api.route('/topthree/', methods = ['GET'])
def topthree():
    def userinfofactory(user):
        return {
            "UserId": user.id,
            "Email": user.email,
            "UserName": user.username,
            "Score": user.score 
        }

    users = User.query.all()
    users = sorted(users, key = lambda u: u.score, reverse=True)
    if len(users) >= 3:
        users = users[:3]
    returninfos = []
    for u in users:
        returninfos.append(userinfofactory(u))
    return jsonify(returninfos)


@api.route('/challenge/', methods = ['GET'])
def challenge():
    myid = request.args.get('myid')
    otherid = request.args.get('otherid')
    user = User.query.filter_by(id = myid).first() or None
    other = User.query.filter_by(id = otherid).first() or None 
    if not user or not other:
        return jsonify({
                'Msg': 'notfound',
                'Code': 400
            })
    
    other.ifchallenge = 1
    other.challenger_id = myid
    return jsonify({
        "Msg": "ok",
        "Code": 200
        })

@api.route('/status/', methods = ['GET'])
def status():
    myid = request.args.get('myid')
    user = User.query.filter_by(id = myid).first() or None
    if not user:
        return jsonify({
                'Msg': 'user not found',
                'Code': '400'
            })

    if user.ifchallenge:
        cu = User.query.filter_by(id = user.challenger_id).first() or None
        return jsonify({
                'Msg': 'yes',
                'UserName': cu.username, 
                'Score': cu.score,
                'Code': 200
            })
    else :
        return jsonify({
                'Msg': 'no',
                'Code': 404
            })


@api.route('/score/', methods = ['POST'])
def score():
    userid = request.get_json().get("UserId")
    score = request.get_json().get("Score")
    user = User.query.filter_by(id = userid).first() or None
    if not user:
        return jsonify({
                'Msg': 'user not found',
                'Code': 400
            })
    user.score = score
    return jsonify({
            'Msg': 'ok',
            'Code': 200
        })
    
